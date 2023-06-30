﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotifyPlaylistApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpotifyPlaylistApp.Controllers
{
    [Route("api/[controller]")]
    public class PlaylistController : Controller
    {

        private SongsContext Db { get; set; }

        public PlaylistController(SongsContext temp)
        {
            Db = temp;
        }

        //GET: api/values
       [HttpGet]
        public async Task<IActionResult> Get(int userid)
        {
            var playlist = await Db.Playlists.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userid);

            var playlistSongs = await Db.PlaylistSongs.Include(x => x.Song).Where(x => x.playListId == playlist.PlaylistId).Select(x => x.Song).ToListAsync();

            var data = new
            {
                user = playlist.User.Username,
                songs = playlistSongs
            };

            return new OkObjectResult(data);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post(int userid, int songId)
        {
            var user = await Db.Users.FirstOrDefaultAsync(x => x.UserId == userid);

            var playlist = await Db.Playlists.FirstOrDefaultAsync(x => x.UserId == userid);

            var instance = new PlaylistSongs
            {
                SongId = songId,
                playListId = playlist.PlaylistId
            };

            Db.PlaylistSongs.Add(instance);

            await Db.SaveChangesAsync();

            return Ok("Added to " + user.Username + "'s playlist");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{uid}/{sid}")]
        public async Task<ActionResult> Delete(int uid, int sid)
        {
            var playlist = await Db.Playlists.FirstOrDefaultAsync(x => x.UserId == uid);

            var instance = await Db.PlaylistSongs.FirstOrDefaultAsync(x => x.playListId == playlist.PlaylistId && x.SongId == sid);

            Db.PlaylistSongs.Remove(instance);

            Db.SaveChanges();

            return Ok("Removed");
        }
    }
}

