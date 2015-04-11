using System.Collections.Generic;
using System.Data.Entity.Migrations;
using DnsTestApp.Model;

namespace DnsTestApp.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DnsTestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DnsTestContext context)
        {
            Node library = new Node {Folder = true, Parent = 0, Text = "Домашняя библиотека"};
            context.Nodes.AddOrUpdate(x => x.Text, library);
            context.SaveChanges();

            Node author;
            List<Node> books;

            author = new Node {Text = "Терри Пратчетт", Parent = library.Id};
            books = new List<Node>
            {
                new Node {Text = "Цвет волшебства"},
                new Node {Text = "Безумная звезда"},
                new Node {Text = "Творцы заклинаний"},
                new Node {Text = "Мор, ученик Смерти"},
                new Node {Text = "Посох и шляпа"}
            };
            AddBooks(context, author, books);

            author = new Node {Text = "Стивен Макконел", Parent = library.Id};
            books = new List<Node>
            {
                new Node {Text = "Совершенный код"}
            };
            AddBooks(context, author, books);

            author = new Node {Text = "Энди Вейр", Parent = library.Id};
            books = new List<Node>
            {
                new Node {Text = "Марсианин"}
            };
            AddBooks(context, author, books);

            author = new Node {Text = "Джеймс Дэшнер", Parent = library.Id};
            books = new List<Node>
            {
                new Node {Text = "Бегущий в лабиринте"},
                new Node {Text = "Сквозь топку"},
                new Node {Text = "Исцеление смертью"}
            };
            AddBooks(context, author, books);

            author = new Node {Text = "Стивен Хокинг", Parent = library.Id};
            books = new List<Node>
            {
                new Node {Text = "Краткая история времени"},
                new Node {Text = "Чёрные дыры и молодые вселенные"},
                new Node {Text = "Мир в ореховой скорлупке"},
                new Node {Text = "Кратчайшая история времени"},
                new Node {Text = "Джордж и тайны Вселенной"}
            };
            AddBooks(context, author, books);
        }

        private void AddBooks(DnsTestContext context, Node author, List<Node> books)
        {
            author.Folder = true;
            context.Nodes.AddOrUpdate(x => x.Text, author);
            context.SaveChanges();

            foreach (Node node in books)
            {
                node.Folder = false;
                node.Parent = author.Id;
                context.Nodes.AddOrUpdate(x => x.Text, node);
            }
        }
    }
}
