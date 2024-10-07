namespace UserServiceApi7.ErrorHandling;

public readonly struct Result<TA> {
	private readonly Exception? _exception;

	/// <summary>Constructor of a concrete value</summary>
	/// <param name="value"></param>
	private Result(TA value) {
		Value = value;
	}

	/// <summary>Constructor of an error value</summary>
	/// <param name="e"></param>
	private Result(Exception? e) {
		_exception = e;
		Value = default!;
	}

	public Exception Error => _exception!;

	[Pure]
	public bool IsError => _exception is not null;

	[Pure]
	public bool IsSuccess => _exception is null;

	public TA Value { get; }

	public void IfFail(Action<Exception> action) {
		if (IsError)
			action(_exception!);
	}

	public async Task IfFailAsync(Func<Exception, Task> action) {
		if (IsError)
			await action(_exception!);
	}

	public void IfSuccess(Action<TA> action) {
		if (IsSuccess)
			action(Value);
	}

	public async Task IfSuccessAsync(Func<TA, Task> action) {
		if (IsSuccess)
			await action(Value);
	}

	[Pure]
	public static implicit operator Result<TA>(TA value) => new(value);

	[Pure]
	public static implicit operator Result<TA>(Exception exc) => new(exc);

	[Pure]
	public override string ToString() {
		if (IsError)
			return _exception?.ToString() ?? "(Bottom)";
		return Value?.ToString() ?? "(null)";
	}


	[Pure]
	public override bool Equals(object? obj) =>
		obj is Result<TA> other && Equals(other);

	public bool Equals(Result<TA> other) {
		if (IsError && other.IsError)
			return _exception == other._exception;
		if (IsSuccess && other.IsSuccess)
			return Value?.Equals(other.Value) ?? false;
		return false;
	}

	[Pure]
	public override int GetHashCode() {
		if (IsError)
			return _exception?.GetHashCode() ?? 0;
		return Value?.GetHashCode() ?? 0;
	}

	public static bool operator ==(Result<TA> left, Result<TA> right) {
		return left.Equals(right);
	}

	public static bool operator !=(Result<TA> left, Result<TA> right) {
		return !(left == right);
	}
}