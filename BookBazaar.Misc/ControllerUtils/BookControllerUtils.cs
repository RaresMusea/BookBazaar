namespace BookBazaar.Misc.ControllerUtils;

public static class BookControllerUtils
{
    public static void TryDeleteBookCoverImage(string coverImageUrl, string rootPath)
    {
        if (!string.IsNullOrEmpty(coverImageUrl))
        {
            var oldImagePath = Path.Combine(rootPath, coverImageUrl.TrimStart('\\'));

            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }
        }
    }
}