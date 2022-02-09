namespace BackgroundTaskNet6.Jobs
{
    public class DemoJobs
    {
        public static void Print()
        {
            Console.WriteLine($"Run Job: {DateTime.Now} ...");
            //Write to a file
            string something = File.ReadAllText("Log.txt");
            using (StreamWriter writer = new StreamWriter("Log.txt"))
            {
                something += $"Run Job: {DateTime.Now} ...\n";
                writer.WriteLine(something);
            }
        }
        public static void Print2()
        {
            Console.WriteLine($"Run Job 2: {DateTime.Now} ...");
            //Write to a file
            string something = File.ReadAllText("Log.txt");
            using (StreamWriter writer = new StreamWriter("Log.txt"))
            {
                something += $"Run Job 2: {DateTime.Now} ...\n";
                writer.WriteLine(something);
            }
        }
    }
}
