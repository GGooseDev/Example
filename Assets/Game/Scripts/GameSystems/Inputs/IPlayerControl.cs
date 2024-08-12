public interface IPlayerControl
{
    public float GetHorizontalAxis { get; }
    public float GetVerticalAxis { get; }
    public bool GetRunning { get; }
    public float GetHorizontalLookAxis { get; }
    public float GetVerticalLookAxis { get; }
}