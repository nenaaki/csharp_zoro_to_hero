# LINQ（Language Integrated Query）

LINQ は、.NET の統合クエリ言語です。配列、リスト、データベースなど、さまざまなデータソースに対して、統一的なクエリ構文を使用できます。

## 基本概念

### LINQ とは

LINQ は、データを操作するための宣言型クエリ言語です。`SQL`のようなクエリ構文や`メソッドチェーン`を使用できます。

```csharp
// サンプルデータ
List<int> numbers = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// クエリ構文
var result1 = from n in numbers
              where n > 5
              select n;

// メソッド構文
var result2 = numbers.Where(n => n > 5);
```

## クエリ構文 vs メソッド構文

### クエリ構文

SQL のような宣言型の書き方です。

```csharp
var query = from student in students
            where student.Age > 20
            orderby student.Name
            select student;
```

### メソッド構文

メソッドチェーンを使った手続き型の書き方です。

```csharp
var query = students
    .Where(s => s.Age > 20)
    .OrderBy(s => s.Name);
```

## よく使う LINQ メソッド

### Where - フィルタリング

条件に合う要素だけを抽出します。

```csharp
List<int> numbers = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
var evenNumbers = numbers.Where(n => n % 2 == 0);
// 結果: 2, 4, 6, 8, 10
```

### Select - 変換

要素を別の形に変換します。

```csharp
var doubled = numbers.Select(n => n * 2);
// 結果: 2, 4, 6, 8, 10, 12, 14, 16, 18, 20

// 複雑な変換
var students = new[] 
{ 
    new { Name = "Alice", Score = 85 },
    new { Name = "Bob", Score = 92 }
};
var names = students.Select(s => s.Name);
// 結果: Alice, Bob
```

### OrderBy / OrderByDescending - ソート

要素を並べ替えます。

```csharp
var sorted = students.OrderBy(s => s.Score);  // 昇順
var sortedDesc = students.OrderByDescending(s => s.Score);  // 降順
```

### First / FirstOrDefault - 最初の要素

最初の要素を取得します。

```csharp
var first = numbers.First();  // 例外が発生する可能性あり
var firstOrDefault = numbers.FirstOrDefault();  // null または 0 を返す

// 条件付き
var firstEven = numbers.First(n => n % 2 == 0);
var firstEvenOrDefault = numbers.FirstOrDefault(n => n % 2 == 0);
```

### Count / Any / All - 集計

要素数や条件をチェックします。

```csharp
var count = numbers.Count();  // 要素数
var countEven = numbers.Count(n => n % 2 == 0);  // 条件に合う要素数

var hasEven = numbers.Any(n => n % 2 == 0);  // 1 つでも条件に合う？
var allPositive = numbers.All(n => n > 0);  // すべて条件に合う？
```

### Sum / Average / Min / Max - 統計

統計情報を計算します。

```csharp
var sum = numbers.Sum();  // 合計
var average = numbers.Average();  // 平均
var min = numbers.Min();  // 最小値
var max = numbers.Max();  // 最大値

// カスタム計算
var totalScore = students.Sum(s => s.Score);
var avgScore = students.Average(s => s.Score);
```

### GroupBy - グループ化

要素をグループ分けします。

```csharp
var grouped = students.GroupBy(s => s.Department);

foreach (var group in grouped)
{
    Console.WriteLine($"{group.Key}: {group.Count()} 人");
}
```

### Join - 結合

2 つのシーケンスを結合します。

```csharp
var departments = new[] 
{ 
    new { Id = 1, Name = "IT" },
    new { Id = 2, Name = "HR" }
};

var joined = students.Join(
    departments,
    s => s.DeptId,
    d => d.Id,
    (s, d) => new { s.Name, d.Name }
);
```

### Distinct - 重複排除

重複を削除します。

```csharp
List<int> nums = new() { 1, 2, 2, 3, 3, 3, 4 };
var unique = nums.Distinct();
// 結果: 1, 2, 3, 4
```

### Take / Skip - 部分抽出

指定数の要素を取得またはスキップします。

```csharp
var first3 = numbers.Take(3);  // 最初の 3 個
var after2 = numbers.Skip(2);  // 最初の 2 個を除外
var paginated = numbers.Skip(10).Take(5);  // ページング
```

## LINQ to Objects

通常のコレクション（配列、リスト）に対する LINQ クエリです。

```csharp
class Program
{
    static void Main()
    {
        List<Product> products = new()
        {
            new("Apple", 100),
            new("Banana", 80),
            new("Orange", 120),
            new("Grape", 200)
        };

        // 100 円以上の商品を取得
        var expensive = products
            .Where(p => p.Price >= 100)
            .OrderBy(p => p.Price)
            .Select(p => new { p.Name, p.Price });

        foreach (var item in expensive)
        {
            Console.WriteLine($"{item.Name}: ¥{item.Price}");
        }
    }
}

class Product
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }
}
```

## 遅延実行と即座実行

LINQ クエリは、デフォルトでは**遅延実行**されます。実際にデータにアクセスするまで実行されません。

```csharp
// 遅延実行
var query = numbers.Where(n => n > 5);

// ここで初めて実行される
foreach (var n in query)
{
    Console.WriteLine(n);
}

// 即座実行（結果をメモリに読み込む）
var list = numbers.Where(n => n > 5).ToList();  // List<int>
var array = numbers.Where(n => n > 5).ToArray();  // int[]
```

## LINQ to SQL / Entity Framework

データベースに対する LINQ クエリです。（詳細は Entity Framework のドキュメント参照）

```csharp
// Entity Framework 例
var context = new MyDbContext();
var activeUsers = context.Users
    .Where(u => u.IsActive)
    .OrderBy(u => u.Name)
    .ToList();
```

## 複合クエリ例

```csharp
class Student
{
    public string Name { get; set; }
    public int Score { get; set; }
    public string Class { get; set; }
}

class Program
{
    static void Main()
    {
        var students = new[]
        {
            new Student { Name = "Alice", Score = 85, Class = "A" },
            new Student { Name = "Bob", Score = 92, Class = "B" },
            new Student { Name = "Charlie", Score = 78, Class = "A" },
            new Student { Name = "Diana", Score = 95, Class = "B" }
        };

        // クラス A の成績が 80 以上の学生を取得し、成績でソート
        var result = students
            .Where(s => s.Class == "A")
            .Where(s => s.Score >= 80)
            .OrderByDescending(s => s.Score)
            .Select(s => new { s.Name, s.Score });

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Name}: {item.Score}");
        }
        // 出力: Alice: 85
    }
}
```

## まとめ

- LINQ は、クエリ構文またはメソッド構文で使用できます
- `Where`、`Select`、`OrderBy`など、多くの便利なメソッドがあります
- クエリは遅延実行されるため、パフォーマンスに注意しましょう
- 配列、リスト、データベースなど、様々なデータソースに対応できます
