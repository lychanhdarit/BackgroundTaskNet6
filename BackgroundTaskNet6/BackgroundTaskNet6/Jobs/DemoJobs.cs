namespace BackgroundTaskNet6.Jobs
{
    public class DemoJobs
    {
        public static void Print()
        {
            Console.WriteLine($"Run Job: {DateTime.Now} ...");
        }
        public static void Print2()
        {
            Console.WriteLine($"Run Job 2: {DateTime.Now} ...");
        }
    }
}
