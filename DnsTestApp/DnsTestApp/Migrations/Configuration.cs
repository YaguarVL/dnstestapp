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
            Node library = new Node {Folder = true, Parent = 0, Text = "�������� ����������"};
            context.Nodes.AddOrUpdate(x => x.Text, library);
            context.SaveChanges();

            Node author;
            List<Node> books;

            author = new Node {Text = "����� ��������", Parent = library.Id};
            books = new List<Node>
            {
                new Node {Text = "���� ����������"},
                new Node {Text = "�������� ������"},
                new Node {Text = "������ ����������"},
                new Node {Text = "���, ������ ������"},
                new Node {Text = "����� � �����"}
            };
            AddBooks(context, author, books);

            author = new Node {Text = "������ ��������", Parent = library.Id};
            books = new List<Node>
            {
                new Node {Text = "����������� ���"}
            };
            AddBooks(context, author, books);

            author = new Node {Text = "���� ����", Parent = library.Id};
            books = new List<Node>
            {
                new Node {Text = "���������"}
            };
            AddBooks(context, author, books);

            author = new Node {Text = "������ ������", Parent = library.Id};
            books = new List<Node>
            {
                new Node {Text = "������� � ���������"},
                new Node {Text = "������ �����"},
                new Node {Text = "��������� �������"}
            };
            AddBooks(context, author, books);

            author = new Node {Text = "������ ������", Parent = library.Id};
            books = new List<Node>
            {
                new Node {Text = "������� ������� �������"},
                new Node {Text = "׸���� ���� � ������� ���������"},
                new Node {Text = "��� � �������� ���������"},
                new Node {Text = "���������� ������� �������"},
                new Node {Text = "������ � ����� ���������"}
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
