using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Template10.SampleData.Email
{
    public class MessageList : ObservableCollection<Message>
    {
        public MessageList()
        {
            var count = 12;

            var names = new Generator.Names.NameList();
            var tos = names.Take(count).Select((x, i) => new { Index = i, Name = new Recipient(x.FullName) });
            var froms = names.Skip(count).Take(count).Select((x, i) => new { Index = i, Name = new Recipient(x.FullName) });

            var subjects = new Generator.Sentences.SentenceList().Take(count).Select((x, i) => new { Index = i, Subject = x.Text });

            var paragraphs = new Generator.Paragraphs.ParagraphList();
            var bodies1 = paragraphs.Take(count).Select((x, i) => new { Index = i, Paragraph = x.Text });
            var bodies2 = paragraphs.Reverse().Take(count).Select((x, i) => new { Index = i, Paragraph = x.Text });

            var random = new Random((int)DateTime.Now.Ticks);
            var dates = Enumerable.Range(1, 24).Select(x => new { Index = x, Date = DateTime.Now.Subtract(TimeSpan.FromDays(random.Next(1, 24))) });

            var samples = from t in tos
                          join f in froms on t.Index equals f.Index
                          join d in dates on t.Index equals d.Index
                          join s in subjects on t.Index equals s.Index
                          join b1 in bodies1 on t.Index equals b1.Index
                          join b2 in bodies2 on t.Index equals b2.Index
                          select new Message
                          {
                              To = t.Name,
                              From = f.Name,
                              Date = d.Date,
                              Subject = (s.Index % 3 == 0) ? $"RE:{s.Subject}" : s.Subject,
                              Body = $"Hello,\r\n\r\n{b1.Paragraph}\r\n\r\n{b2.Paragraph}"
                          };
            foreach (var sample in samples)
            {
                Add(sample);
            }
        }
    }
}
