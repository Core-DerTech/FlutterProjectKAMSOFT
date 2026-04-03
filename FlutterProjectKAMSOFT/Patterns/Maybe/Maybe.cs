namespace FlutterProjectKAMSOFT.Patterns.Maybe
{
    public abstract record Maybe<T>
    {
        public static Maybe<T> Some(T value) => new Some<T>(value);
        public static Maybe<T> None() => new None<T>(default!);
    }

    public record Some<T>(T Value) : Maybe<T>;
    public record None<T>(T Value) : Maybe<T>;
}
