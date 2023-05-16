namespace DefaultNamespace;

public class PageManager
{
    private IPage currentPage;

    public void NavigateToPage(IPage page)
    {
        // Logic to navigate to the specified page
    }

    public void SetCurrentPage(IPage page)
    {
        currentPage = page;
    }

    public IPage GetCurrentPage()
    {
        return currentPage;
    }
}
