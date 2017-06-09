using Template10.SampleData.Mvvm;

namespace Template10.SampleData.Email
{
    public class Recipient : BindableBase
    {
        internal Recipient(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public string Email => $"{Name.Replace(" ", "_")}@email.com";

        public override string ToString()
        {
            return Name;
        }
    }
}
