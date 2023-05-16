namespace DefaultNamespace;

public interface IPage
{
    string PageTitle { get; set; }

    void Render();

    // Other methods and properties specific to a page
}
