namespace Sentience
{
    public class PageSegment
    {
        public bool IsVisible { get; set; } = false;
        public string Name { get; set; }
        public void Toggle()
        {
            IsVisible = !IsVisible;
        }
    }
}
