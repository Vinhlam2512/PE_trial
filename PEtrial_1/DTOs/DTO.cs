using PEtrial_1.Models;

namespace PEtrial_1.DTOs {
    public class DTO {
        public class CreateDirectorDto {
            public string FullName { get; set; } = null!;
            public bool Male { get; set; }
            public string DobString { get; set; }
            public string Nationality { get; set; } = null!;
            public string Description { get; set; } = null!;
        }

        public class DirectorDTO {
            public int Id { get; set; }
            public string FullName { get; set; } = null!;
            public string Gender { get; set; }
            public DateTime Dob { get; set; }
            public string DobString { get; set; }
            public string Nationality { get; set; } = null!;
            public string Description { get; set; } = null!;
            public virtual ICollection<MovieDTO> Movies { get; set; }
        }

        public class MovieDTO {
            public int Id { get; set; }
            public string Title { get; set; } = null!;
            public DateTime? ReleaseDate { get; set; }
            public int ReleaseYear { get; set; }
            public string? Description { get; set; }
            public string Language { get; set; } = null!;
            public int? ProducerId { get; set; }
            public int? DirectorId { get; set; }
            public string ProducerName { get; set; }
            public string DirectorName { get; set; }

            public virtual ICollection<Genre> Genres { get; set; }
            public virtual ICollection<Star> Stars { get; set; }
        }
    }

}
