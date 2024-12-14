namespace HR.Domain.Shared.ValueObjects;

public class Phone(int number)
{
  public int Number { get; } = number;

  public static implicit operator Phone(string number) 
    => new(int.Parse(number.Replace("-", string.Empty)));

  public override string ToString() => Number.ToString("000-000-0000");
}
