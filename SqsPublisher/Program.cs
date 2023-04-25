using Amazon.SQS;

namespace SqsPublisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AmazonSQSClient client = new AmazonSQSClient();
        }
    }
}