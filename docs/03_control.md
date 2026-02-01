# 制御構造 — 条件分岐とループ

この章では C# の代表的な制御構造（`if` / `switch` / `for` / `foreach` / `while` / `do-while`）を解説します。簡単な例と共に、最近のパターンマッチングや `switch` 式についても触れます。

## if / else

```csharp
int x = 10;
if (x > 0)
{
    Console.WriteLine("正の数");
}
else if (x == 0)
{
    Console.WriteLine("ゼロ");
}
else
{
    Console.WriteLine("負の数");
}
```

Null 合体やパターンを組み合わせた例:

```csharp
string? name = GetName();
var display = name ?? "(無名)";
if (display is string s && s.Length > 0)
    Console.WriteLine(s);
```

## switch 文と switch 式

従来の `switch`:

```csharp
int code = 2;
switch (code)
{
    case 1:
        Console.WriteLine("One");
        break;
    case 2:
        Console.WriteLine("Two");
        break;
    default:
        Console.WriteLine("Other");
        break;
}
```

`switch` 式（C# 8+）:

```csharp
var result = code switch
{
    1 => "One",
    2 => "Two",
    _ => "Other"
};
Console.WriteLine(result);
```

型パターンを使った `switch`:

```csharp
object obj = 123;
switch (obj)
{
    case int i:
        Console.WriteLine($"int: {i}");
        break;
    case string s:
        Console.WriteLine($"string: {s}");
        break;
    default:
        Console.WriteLine("unknown type");
        break;
}
```

## for / foreach

標準的な `for` ループ:

```csharp
for (int i = 0; i < 5; i++)
{
    Console.WriteLine(i);
}
```

配列やコレクションの走査には `foreach` を使う:

```csharp
var arr = new[] { 10, 20, 30 };
foreach (var item in arr)
{
    Console.WriteLine(item);
}
```

## while / do-while

`while`:

```csharp
int n = 0;
while (n < 3)
{
    Console.WriteLine(n);
    n++;
}
```

`do-while`（必ず1回実行される）:

```csharp
int m = 0;
do
{
    Console.WriteLine(m);
    m++;
} while (m < 3);
```

## break / continue / goto

- `break` は最も内側のループや `switch` を抜けます。  
- `continue` はそのループの次の反復へ進みます。  
- `goto` は一般には避けるが、ラベルジャンプが必要な特別なケースでは使える。

```csharp
for (int i = 0; i < 10; i++)
{
    if (i % 2 == 0) continue; // 偶数はスキップ
    if (i > 7) break; // 8 以上で終了
    Console.WriteLine(i);
}
```

## パターンマッチングの例（応用）

```csharp
object? value = GetValue();
if (value is int vi && vi > 0)
    Console.WriteLine("正の整数: " + vi);

// switch と組み合わせる
switch (value)
{
    case null:
        Console.WriteLine("null です");
        break;
    case string s when s.Length > 0:
        Console.WriteLine("string: " + s);
        break;
    case int i:
        Console.WriteLine("int: " + i);
        break;
}
```

## 小さな例: 合計と最大値

```csharp
int[] numbers = { 3, 7, 1, 9 };
int sum = 0;
int max = int.MinValue;
foreach (var v in numbers)
{
    sum += v;
    if (v > max) max = v;
}
Console.WriteLine($"合計: {sum}, 最大: {max}");
```

## 練習課題

1. FizzBuzz: 1 から 100 までを出力。3 の倍数は `Fizz`、5 の倍数は `Buzz`、両方なら `FizzBuzz` を出力。
2. 配列から偶数だけを抽出して新しいリストを作るプログラムを作成せよ。
3. `switch` 式を使って数値を文字列に変換するマッピングを作成せよ（例: 1→"One", 2→"Two", それ以外→"Other"）。

---

作成日: 2026-02-01
