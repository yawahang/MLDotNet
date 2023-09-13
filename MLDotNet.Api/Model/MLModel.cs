namespace MLDotNet.Api.Model
{
    public class MvMLInput
    {
        public byte[] Image;
    }

    public class MvMLOutput
    {
        public bool Sentiment { get; set; }

        public float Probability { get; set; }

        public float Score { get; set; }
    }
}
