namespace AutomationFramework.Util.Behrang.CustomReport
{
    class TestResult
    {
        public string TestStep { get; set; }
        public string Comment { get; set; }
        public Status TestStatus { get; set; }
        public string ElapsedTime { get; set; }
        public override string ToString()
        {
            return $"{nameof(TestStep)}: {TestStep}\r\n{nameof(Comment)}: {Comment}\r\n" +
                   $"{nameof(TestStatus)}: {TestStatus}\r\n{nameof(ElapsedTime)}: {ElapsedTime}";
        }
    }

    public enum Status
    {
        Pass,Fail, Pending
    }
}

