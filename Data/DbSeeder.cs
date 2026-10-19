using BootNews.Models;

namespace BootNews.Data;

/// <summary>
/// DbSeeder — wuxuu database-ka ku buuxiyaa xog tijaabo ah
/// haddii uu madhan yahay (kaliya markii ugu horreysa).
/// </summary>
public static class DbSeeder
{
    private const string Body = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.

It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker.

Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur.

The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from ""de Finibus Bonorum et Malorum"" by Cicero are also reproduced in their exact original form.";

    private static string Img(string id, int w = 800) =>
        $"https://images.unsplash.com/photo-{id}?auto=format&fit=crop&w={w}&q=80";

    private static string MakeSlug(string title) =>
        string.Join('-', title.ToLowerInvariant()
            .Replace("'", "").Replace("–", " ").Replace(":", " ").Replace(",", " ").Replace("?", " ")
            .Split(' ', StringSplitOptions.RemoveEmptyEntries).Take(8));

    public static void Seed(NewsDbContext db)
    {
        if (db.Categories.Any()) return;

        // ---- Categories ----
        var business = new Category { Name = "Business", Slug = "business" };
        var science = new Category { Name = "Science", Slug = "science" };
        var travel = new Category { Name = "Travel", Slug = "travel" };
        var lifestyle = new Category { Name = "Lifestyle", Slug = "lifestyle" };
        var sports = new Category { Name = "Sports", Slug = "sports" };
        var automotive = new Category { Name = "Automotive", Slug = "automotive" };
        var tech = new Category { Name = "Tech", Slug = "tech" };
        var international = new Category { Name = "International", Slug = "international" };
        db.Categories.AddRange(business, science, travel, lifestyle, sports, automotive, tech, international);

        // ---- Authors ----
        var john = new Author { Name = "John Doe", Bio = "Senior editor at BootNews." };
        var jane = new Author { Name = "Jane Smith", Bio = "Tech and science reporter." };
        var alex = new Author { Name = "Alex Ray", Bio = "Business correspondent." };
        db.Authors.AddRange(john, jane, alex);

        // ---- Tags ----
        var tagNames = new[] { "Finance", "Dollar", "Economy", "Market", "World", "Health", "AI", "Racing", "Football", "Food" };
        var tags = tagNames.Select(n => new Tag { Name = n }).ToList();
        db.Tags.AddRange(tags);

        db.SaveChanges(); // si IDs loogu daro

        // ---- Articles ----
        var articles = new List<Article>();
        void Add(string title, Category cat, Author author, string img, int daysAgo,
                 bool hero = false, bool grid = false, bool breaking = false, bool popular = false, int views = 0, int comments = 0)
        {
            articles.Add(new Article
            {
                Title = title,
                Slug = MakeSlug(title),
                Summary = "This is a wider card with supporting text below as a natural lead-in to additional content. A small river flows by their place and ...",
                Content = Body,
                ImageUrl = Img(img),
                PublishedAt = new DateTime(2019, 10, 22).AddDays(-daysAgo),
                Views = views,
                CommentCount = comments,
                IsHero = hero,
                IsHomeGrid = grid,
                IsBreaking = breaking,
                IsPopular = popular,
                Category = cat,
                Author = author
            });
        }

        // Hero carousel
        Add("Superstar Calls For Football Clean Up Amid Series Of Shocking Allegations", sports, john, "1522778119026-d647f0596c20", 0, hero: true, views: 4210, comments: 12);
        Add("New Technology Is Changing The Way We Watch Live Sports", tech, jane, "1519389950473-47ba0277781c", 1, hero: true, views: 2310, comments: 5);
        Add("Global Markets Rally As Trade Talks Show Signs Of Progress", business, alex, "1611974789855-9c2a0a7236a3", 2, hero: true, views: 1870, comments: 8);

        // Home grid cards
        Add("Should you see the Fantastic Beasts sequel?", business, john, "1496181133206-80ce9b88a853", 0, grid: true, views: 980, comments: 3);
        Add("Three myths about Florida elections recount", science, jane, "1532094349884-543bc11b234d", 0, grid: true, views: 1340, comments: 6);
        Add("Finding Empowerment in Two Wheels and a Helmet", travel, john, "1488646953014-85cb44e25828", 1, grid: true, views: 760, comments: 2);
        Add("Ditch receipts and four other tips to be a green shopper", lifestyle, jane, "1483985988355-763728e1935b", 1, grid: true, views: 1120, comments: 4);

        // Sports section
        Add("GB's Bankes wins snowboard cross silver at World Champs", sports, john, "1551698618-1dfe5d97d256", 0, popular: true, views: 3210, comments: 9);
        Add("Yankees Party Like It's 1998, at Least for a Day", sports, john, "1549719386-74dfcbf7dbed", 0, views: 2140, comments: 7);
        Add("Dale Earnhardt Jr. Returns to the Racetrack and an Old Hobby", sports, jane, "1552519507-da3b142c6e3d", 1, popular: true, views: 1980, comments: 3);
        Add("Carmelo Anthony Officially Signs with the Houston Rockets", sports, john, "1546519638-68e109498ffc", 2, views: 2890, comments: 11);
        Add("Jacob deGrom Goes the Distance as Mets Top the Phillies", sports, alex, "1574629810360-7efbbe195018", 5, views: 1540, comments: 2);

        // Breaking news
        Add("Investors Betting Against Tesla Made $1 Billion on Friday", business, alex, "1526304640581-d334cdbbf45e", 0, breaking: true, views: 5120, comments: 21);
        Add("Google Employees Protest Secret Work on Censored Search Engine for China", tech, jane, "1518770660439-4636190af475", 1, breaking: true, popular: true, views: 6310, comments: 34);
        Add("5 Takeaways From Elon Musk's Interview With The Times About Tesla", tech, john, "1451187580459-43490279c0fa", 1, breaking: true, popular: true, views: 4420, comments: 18);
        Add("Lemon make your skin fresh and glowing", lifestyle, jane, "1490474418585-ba9bad8fd0ea", 2, breaking: true, views: 980, comments: 5);

        // Category page fillers (Business)
        Add("Apple's stock rout starts and ends with the iPhone", business, john, "1460925895917-afdab827c52f", 1, views: 2870, comments: 9);
        Add("Want the best Black Friday deals? Head to a department store", business, alex, "1441986300917-64674bd600d8", 1, views: 1760, comments: 4);
        Add("5 Tips to Save Money Booking Your Next Hotel Room", business, john, "1566073771259-6a8506099945", 2, views: 1230, comments: 2);
        Add("Dubai launches pilot for Middle East Shark Tank", international, alex, "1512453979798-5ea266f8880c", 2, views: 1450, comments: 6);

        // Automotive / popular
        Add("The pioneering and popular compact Range Rover Evoque gets a major makeover", automotive, jane, "1503376780353-7e6692767b70", 3, popular: true, views: 3890, comments: 12);
        Add("Meet The Man Who Designed The Ducati Monster", automotive, john, "1568772585407-9361f9bf3a87", 4, popular: true, views: 2740, comments: 8);
        Add("2020 Audi R8 Spyder spy shots release", automotive, jane, "1542362567-b07e54358753", 5, views: 1980, comments: 5);
        Add("MotoGP: Which team needs what in 2019?", sports, john, "1558981403-c5f9899a28bc", 6, views: 2210, comments: 7);

        // Travel / lifestyle fillers
        Add("Take the kids to Windermere Jetty, Lake District", travel, jane, "1476514525535-07fb3b4ae5f1", 6, views: 890, comments: 1);
        Add("Wave reviews: Under, Norway's new underwater restaurant", travel, alex, "1414235077428-338989a2e8c0", 7, views: 1340, comments: 3);
        Add("This U.S. Airline Has More Legroom Than Any Other", travel, john, "1436491865332-7a61a109cc05", 8, views: 1670, comments: 4);
        Add("5 Simple Tips to Help Vegetarian or Vegan Travelers Eat Well, Anywhere", lifestyle, jane, "1512621776951-a57141f2eefd", 9, views: 1230, comments: 5);

        db.Articles.AddRange(articles);
        db.SaveChanges();

        // ---- ArticleTags (tusaale: Business => Finance, Dollar, Economy, Market, World) ----
        var tagByName = tags.ToDictionary(t => t.Name);
        void Tag(Article a, params string[] names)
        {
            foreach (var n in names)
                db.ArticleTags.Add(new ArticleTag { ArticleId = a.Id, TagId = tagByName[n].Id });
        }

        foreach (var a in articles.Where(a => a.CategoryId == business.Id))
            Tag(a, "Finance", "Dollar", "Economy", "Market", "World");
        Tag(articles.First(a => a.Title.StartsWith("Superstar")), "Football", "World");
        Tag(articles.First(a => a.Title.StartsWith("MotoGP")), "Racing");
        Tag(articles.First(a => a.Title.StartsWith("Lemon")), "Health", "Food");
        Tag(articles.First(a => a.Title.StartsWith("5 Simple Tips")), "Health", "Food");
        Tag(articles.First(a => a.Title.StartsWith("Google Employees")), "AI", "World");
        Tag(articles.First(a => a.Title.StartsWith("New Technology")), "AI");

        db.SaveChanges();
    }
}
