namespace Code.Runtime.Services.InputService
{
    public interface IInputProvider<out TInput>
    {
        TInput Input { get; }
    }
}