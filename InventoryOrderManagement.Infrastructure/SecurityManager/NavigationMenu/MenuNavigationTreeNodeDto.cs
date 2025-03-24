namespace InventoryOrderManagement.Infrastructure.SecurityManager.NavigationMenu;

public class MenuNavigationTreeNodeDto
{
    public string Id { get; set; }
    public string Text { get; set; }
    public string? ParentId { get; set; }
    public string? URL { get; set; }
    public bool HasChild { get; set; }
    public bool Expanded { get; set; }

    public MenuNavigationTreeNodeDto(string id, string text, string? parentId = null, string? url = null, bool param_hasChild = false, bool param_expanded = false)
    {
        Id = id;
        Text = text;
        ParentId = parentId;
        URL = url;
        HasChild = param_hasChild;
        Expanded = param_expanded;
    }
} 