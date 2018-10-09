using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using AutomationFramework.Util.Behrang.CustomReport;
using TechTalk.SpecFlow;

namespace AutomationFramework.DevTests.Steps
{
    [Binding]
    class TestGenerateHtmlReportStepDef
    {
        private List<TestResult> _testresults;

 

        [Given(@"I have entered collection of test results")]
        public void GivenIHaveEnteredCollectionOfTestResults()
        {
            _testresults= new List<TestResult>();
        }

        [Then(@"I Can Generate the html Report")]
        public void ThenICanGenerateTheHtmlReport()
        {
            var watch = new Stopwatch();
            watch.Start();
            Thread.Sleep(100);
            
            var t1 = new TestResult
            {
                  TestStatus = Status.Pass,
                  ElapsedTime = (watch.ElapsedMilliseconds).ToString(),
                  Comment = "Comment 1",
                  TestStep = "Test Step 1"

            };
            Thread.Sleep(300);
            var t2 = new TestResult
            {
                TestStatus = Status.Pass,
                ElapsedTime = (watch.ElapsedMilliseconds).ToString(),
                Comment = "Comment 2",
                TestStep = "Test Step 2"
            };
            Thread.Sleep(400);
            var t3 = new TestResult
            {
                TestStatus = Status.Fail,
                ElapsedTime = (watch.ElapsedMilliseconds ).ToString(),
                Comment = "Comment 3",
                TestStep = "Test Step 3"
            };
            Thread.Sleep(500);
            var t4 = new TestResult
            {
                TestStatus = Status.Pending,
                ElapsedTime = (watch.ElapsedMilliseconds ).ToString(),
                Comment = "Comment 4",
                TestStep = "Test Step 4"
            };
            Thread.Sleep(300);
            _testresults.Add(t1);
            _testresults.Add(t2);
            _testresults.Add(t3);
            _testresults.Add(t4);
            watch.Stop();
            var report = new HtmlCreator();
            report.BuildReport(_testresults,watch);
        }

    }
}
