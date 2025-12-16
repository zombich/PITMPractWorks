using ClassLibraryPract4;
using System;
using System.Collections.Generic;
using System.Linq;

public static class NotificationFilter
{
    public static IEnumerable<Notification> FilterAndSort(
        IEnumerable<Notification> notifications,
        NotificationFilterOptions options)
    {
        // Фильтрация
        var filtered = notifications.AsQueryable();

        if (options.IsRead.HasValue)
        {
            filtered = filtered.Where(n => n.IsRead == options.IsRead.Value);
        }

        if (options.Types != null && options.Types.Length > 0)
        {
            filtered = filtered.Where(n => options.Types.Contains(n.Type));
        }

        if (!string.IsNullOrEmpty(options.SearchText))
        {
            filtered = filtered.Where(n => n.Title.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase) ||
                                            (n.Content != null && n.Content.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase)));
        }

        if (options.MinPriority.HasValue)
        {
            filtered = filtered.Where(n => n.Priority >= options.MinPriority.Value);
        }

        // Сортировка
        switch (options.SortBy)
        {
            case SortNotificationBy.Date:
                filtered = options.Descending ? filtered.OrderByDescending(n => n.CreatedAt) : filtered.OrderBy(n => n.CreatedAt);
                break;
            case SortNotificationBy.Priority:
                filtered = options.Descending ? filtered.OrderByDescending(n => n.Priority) : filtered.OrderBy(n => n.Priority);
                break;
            case SortNotificationBy.Title:
                filtered = options.Descending ? filtered.OrderByDescending(n => n.Title) : filtered.OrderBy(n => n.Title);
                break;
            default:
                break;
        }

        return filtered.ToList();
    }
}

