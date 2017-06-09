using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Template10.SampleData.Mvvm;

namespace Template10.SampleData.Email
{
    public class Message : BindableBase
    {
        Recipient _From = default(Recipient);
        public Recipient From { get => _From; set => Set(ref _From, value); }

        Recipient _To = default(Recipient);
        public Recipient To { get => _To; set => Set(ref _To, value); }

        DateTime _Date = default(DateTime);
        public DateTime Date { get => _Date; set => Set(ref _Date, value); }

        string _Subject = default(string);
        public string Subject { get => _Subject; set => Set(ref _Subject, value); }

        string _Body = default(string);
        public string Body { get => _Body; set => Set(ref _Body, value); }
    }
}
