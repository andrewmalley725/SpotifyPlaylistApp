using System;
using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistApp.Models
{
	public class User
	{
		[Key]
		[Required]
		public int UserId { get; set; }

		[Required]
		public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

    }
}

