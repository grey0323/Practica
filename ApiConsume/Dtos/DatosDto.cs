namespace ApiConsume.Dtos
{
    public class DatosDto
    {
        public int Id { get; set; }

        public string Link { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime Date { get; set; }
    }
}
