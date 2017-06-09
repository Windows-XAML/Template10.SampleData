namespace Template10.SampleData.Generator.Sentences
{
    public class Sentence
    {
        internal Sentence(string sentence)
        {
            Text = sentence?.Trim();
        }

        public string Text { get; }
        public override string ToString()
        {
            return Text;
        }
    }
}
