using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
   public  class BookData
    {
       private static List<Book> _allBooks = new List<Book>();
        static BookData()
       {
           _allBooks.Add(new Book
           {
               Id = Guid.NewGuid(),
               Title = "平凡的世界",
               Author = "路遥",
               Description = "《平凡的世界（套装共3册）》是一部现实主义小说，也是小说化的家族史。作家高度浓缩了中国西北农村的历史变迁过程，作品达到了思想性与艺术性的高度统一，特别是主人公面对困境艰苦奋斗的精神，对今天的大学生朋友仍有启迪。",
               Pages = 371
           });
           _allBooks.Add(new Book
           {
               Id = Guid.NewGuid(),
               Title = "白鹿原",
               Author = "陈忠实",
               Description = "《白鹿原》是一部渭河平原五十年变迁的雄奇史诗，一轴中国农村班斓多彩、触目惊心的长幅画卷。……厚重深邃的思想内容，复杂多变的人物性格，跌宕曲折的故事情节，绚丽多彩的风土人情，形成作品鲜明的艺术特色和令人震撼的真实感。",
               Pages = 371
           });

           _allBooks.Add(new Book
           {
               Id = Guid.NewGuid(),
               Title = "瓦尔登湖",
               Author = "梭罗",
               Description = "《瓦尔登湖》是一本超凡入圣的好书，严重的污染使人们丧失了田园的宁静，所以梭罗的著作便被整个世界阅读和怀念了。",
               Pages = 371
           });

           _allBooks.Add(new Book
           {
               Id = Guid.NewGuid(),
               Title = "老人与海",
               Author = "海明威",
               Description = "《老人与海》所塑造的“硬汉”形象圣地亚哥是海明威所崇尚的完美的人的象征，他坚强、宽厚、乐观、充满自信、热爱生活，即使在人生的角斗场上失败了，面对不可逆转的命运，他仍然是精神上的强者。这一形象激励了一代又一代人，也是本书经久不衰的一个重要原因。",
               Pages = 371
           });

           _allBooks.Add(new Book
           {
               Id = Guid.NewGuid(),
               Title = "世界上的另一个你",
               Author = "朗·霍尔（Ron Hall），丹佛·摩尔（Denver Moore）",
               Description = "这是个真实的故事，故事里闪过真实的人生片段──贪婪、恐惧、苦多于乐、希望、惊喜。《世界上的另一个你》蝉联纽约时报非小说类畅销书排行榜长达3年，至今屹立不摇。一个比小说更像小说的真实故事！",
               Pages = 371
           });
       }

       public static bool AddBook( Book book) 
       {
           _allBooks.Add(book);
           return true;
       }
       public static IEnumerable<Book> GetBook() 
       {
           return _allBooks;
       }

       public static Book GetBookById(Guid id)
       {
           return _allBooks.FirstOrDefault(b => b.Id == id);
       }

       public static bool DeleteBook(Book book) 
       {
           _allBooks.Remove(book);
           return true;
       }
       public static bool UpdateBook(Book book) 
       {    
           var trbook = _allBooks.FirstOrDefault(b => b.Id == book.Id);
           var index = _allBooks.IndexOf(trbook);
           _allBooks.Remove(trbook);
           _allBooks.Insert(index, book);
           return true;
       }
    }
}
