using System;
using System.Collections.Generic;

class Category
{
    public string Name { get; set; }
    public int Products { get; set; } // товарів безпосередньо в цій категорії
    public List<Category> Children { get; set; }

    public Category(string name, int products, List<Category> children = null)
    {
        Name = name;
        Products = products;
        Children = children ?? new List<Category>();
    }
}

class Program
{
    // Задане дерево категорій
    static Category catalog = new Category("Каталог", 0, new List<Category>
    {
        new Category("Одяг", 0, new List<Category>
        {
            new Category("Верхній одяг", 0, new List<Category>
            {
                new Category("Куртки", 12, new List<Category>()),
                new Category("Пальта", 7, new List<Category>())
            }),
            new Category("Светри", 15, new List<Category>())
        }),
        new Category("Взуття", 4, new List<Category>
        {
            new Category("Кросівки", 23, new List<Category>()),
            new Category("Чоботи", 9, new List<Category>())
        }),
        new Category("Аксесуари", 0, new List<Category>
        {
            new Category("Сумки", 18, new List<Category>()),
            new Category("Ремені", 5, new List<Category>())
        })
    });

    static int callCount = 0; // лічильник викликів

    // TODO 1: вивести всю ієрархію з відступом за рівнем вкладеності
    static void PrintTree(Category node, int level)
    {
        callCount++;
        // Виводимо поточну категорію з відступом (два пробіли на рівень)
        Console.WriteLine(new string(' ', level * 2) + node.Name);

        foreach (var child in node.Children)
        {
            PrintTree(child, level + 1);
        }
    }

    // TODO 2: повернути загальну кількість товарів у вузлі та всіх його підкатегоріях
    static int CountProducts(Category node)
    {
        callCount++;
        int total = node.Products;
        foreach (var child in node.Children)
        {
            total += CountProducts(child);
        }
        return total;
    }

    // TODO 3: повернути глибину найдовшої гілки (Каталог сам по собі глибина 1)
    static int MaxDepth(Category node)
    {
        callCount++;
        // Базовий випадок: якщо немає підкатегорій, глибина дорівнює 1
        if (node.Children.Count == 0)
        {
            return 1;
        }

        int maxChildDepth = 0;
        foreach (var child in node.Children)
        {
            int depth = MaxDepth(child);
            if (depth > maxChildDepth)
            {
                maxChildDepth = depth;
            }
        }
        return 1 + maxChildDepth;
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("--- Вивід ієрархії ---");
        PrintTree(catalog, 0);

        // Скидаємо або вираховуємо загальний callCount окремо для кожної задачі, 
        // або дивимося сумарний лічильник після виконання всіх функцій:
        int totalProducts = CountProducts(catalog);
        int depth = MaxDepth(catalog);

        Console.WriteLine($"\nЗагальна кількість товарів: {totalProducts}");
        Console.WriteLine($"Максимальна глибина: {depth}");
        Console.WriteLine($"Загальна кількість викликів (callCount): {callCount}");
    }
}
