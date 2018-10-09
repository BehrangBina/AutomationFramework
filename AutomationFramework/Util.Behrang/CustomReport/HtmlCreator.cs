using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using AutomationFramework.Util.Behrang.ConfigurationHelper;
using log4net;

namespace AutomationFramework.Util.Behrang.CustomReport
{
    class HtmlCreator
    {
        private string _html;
        private static readonly ILog Log = LogManager.GetLogger(typeof(HtmlCreator));
        public HtmlCreator()
        {
             
        }


        private void GetHtmlHead(int pass,int fail,int pending)
        {
            _html = @"
            <html>
            <head>
            <!-- BOOTSTRAP -->
            <meta charset='utf-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no'>
                <meta name='description' content='Test Execution Report'>
                <meta name='author' content='Behrang Bina'>
                 <link rel='icon' href='Style/img/ico.png'>

                 <link href='Style/css/bootstrap.min.css' rel='stylesheet'>

                  <!-- CSS -->
                  <link href='Style/css/cover.css' rel='stylesheet'>

                  <!--Load the AJAX API-->
                  <script type='text/javascript' src='Style/js/loader.js'></script>

                <!--Load the AJAX API-->
                <script type='text/javascript' src='Style/js/loader.js'></script>
                <script type='text/javascript'>
                 // Load the Visualization API and the corechart package.
                    google.charts.load('current', { 'packages': ['corechart'] });

                    // Set a callback to run when the Google Visualization API is loaded.
                    google.charts.setOnLoadCallback(drawChart);

                    // Callback that creates and populates a data table,
                    // instantiates the pie chart, passes in the data and
                    // draws it.
                    function drawChart() {

                    // Create the data table.
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Test Result');
                    data.addColumn('number', 'Test Cases');
                    
                    data.addRows([";
                     _html += $"['PASS',{pass}],";
                     _html += $"['FAIL',{fail}]," ;
                     _html += $"['PENDING',{pending}]";
                     _html+="])";
        }

        public void BuildReport(List<TestResult> testResultList, Stopwatch watch)
        {

            var table = new StringBuilder();
            table.AppendLine(_html);
            table.AppendLine("  <table class='table table-dark'>");
            table.AppendLine("  <thead>");
            table.AppendLine("<tr>");
            table.AppendLine("<th> Test Step </th>");
            table.AppendLine("<th> Comment </th>");
            table.AppendLine("<th> Test Status </th>");
            table.AppendLine("<th> Elapsed Time </ th>");
            table.AppendLine(" </tr>  ");
            table.AppendLine(" </thead>");
            table.AppendLine(" <tbody>");
            var passCount = testResultList.Count(x => x.TestStatus == Status.Pass);
            var failCount = testResultList.Count(x => x.TestStatus == Status.Fail);
            var pendingCount = testResultList.Count(x => x.TestStatus == Status.Pending);
            GetHtmlHead(passCount, failCount, pendingCount);
            foreach (var testresult in testResultList)
            {
                table.AppendLine("<tr>");
                table.AppendLine("<td>").Append(testresult.TestStep).Append("</td>");
                table.AppendLine("<td>").Append(testresult.Comment).Append("</td>");
                table.AppendLine("<td>").Append(testresult.TestStatus).Append("</td>");
                table.AppendLine("<td>").Append(testresult.ElapsedTime).Append("</td>");
                table.AppendLine("</tr>");
                Log.Info("Adding\r\n"+testresult);
            }
            
            table.AppendLine("</tbody>");
            table.AppendLine("</table>");
            double time = watch.ElapsedMilliseconds;
            time = time / 1000;
            table.AppendLine($"<p>Time Spent: {time}S</p>");
            var html = GetFooter(table.ToString(),passCount,failCount,pendingCount);
            html = _html + html;
            var appconfig=new AppConfigHandler();
            var reportPath = appconfig.ReadFolderPathFromConfigurationFile(SolutionSubFolder.Reports);
            var dateTime =DateTime.Now.ToString("MMMM dd  yyyy hh mm ss");
            try
            {
                reportPath =Path.Combine(reportPath , dateTime + ".html");
                File.WriteAllText(reportPath,html);
            }
            catch (Exception e)
            {
                
                Log.Error(e);
                throw;
            }
           


        }

        private string GetFooter(string tableBody, int pass, int fail, int pending)
        {
            var footerHtml = @"
                

                            // Set chart options
                            var options = {
                                'title': 'Test Summary Report',
                                'is3D': true,
                                'legend': 'middle',
                                'width': 400,
                                'height': 300,
                                slices: {
                                    0: { color: '#28a745', offset: 0.1 },
                                    1: { color: '#dc3545', offset: 0.1},
                                    2: { color: '#6c757d', offset: 0.1 }
                                },
                                chartArea: { left: 200 }
                            };

                            // Instantiate and draw our chart, passing in some options.
                            var chart = new window.google.visualization.PieChart(window.document.getElementById('chart_div'));
                            chart.draw(data, options);
                        }
                    </script>
    
                </head>
                <body class='text-center'>
                <div class='cover-container d-flex w-100 h-100 p-3 mx-auto flex-column'>
                    <header class='masthead mb-auto'>
                        <div>
                            <h3 class='masthead-brand'>Test Summary Report</h3>
                            <nav class='nav nav-masthead justify-content-center'>
                                <a class='nav-link active' href='#'>Summary</a>
                                <a class='nav-link' href='#'>Contact</a>
                            </nav>
                        </div>
                    </header>
    
                    <div class='container'>";



              var p2=      @"
                    </div>
                    <br/><br/>
                    <div class='card text-center'>
                        <div class='card-footer text-muted'>
                            Test Summary Report
                        </div>
                        <div class='card-body'>
                            <div class='card-text' id='chart_div' style='text-align: center'></div>";
            p2 += "<div class='row'>";

            p2 += $"<div class='col-sm'><button type='button' class='btn badge-success'>" +
                  $"{Status.Pass}:  <span class='badge badge-light'>{pass}</span>" +
                  " </button></div>";

            p2 += $"<div class='col-sm'><button type='button' class='btn badge-danger'>" +
                  $"{Status.Fail}:  <span class='badge badge-light'>{fail}</span>" +
                  " </button></div>";

            p2 += $"<div class='col-sm'><button type='button' class='btn badge-secondary'>" +
                  $"{Status.Pending}:  <span class='badge badge-light'>{pending}</span>" +
                  " </button></div>";
            p2 += "</row>";
            p2 += "<div class='row'><div class='col-sm-12'></div></row>";
           p2 +=  @"</div>
                        <div class='card-footer text-muted'>
                            <script>
                                // Use of Date.now() function 
                                var d = Date(Date.now());
                                // Converting the number of millisecond in date string 
                                a = d.toString();
                                // Printing the current date                     
                                document.write(a);
                            </script>
                        </div>
                    </div>
                </div>
                </body>

                </html>

                ";

            return footerHtml+tableBody+p2;
        }
    }



}
