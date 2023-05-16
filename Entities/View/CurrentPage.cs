namespace DefaultNamespace;

public class CurrentPage
{
    private PageManager pageManager;

    public CurrentPage(PageManager pageManager)
    {
        this.pageManager = pageManager;
    }

    public IPage GetPageConfiguration()
    {
        return pageManager.GetCurrentPage();
    }
}
