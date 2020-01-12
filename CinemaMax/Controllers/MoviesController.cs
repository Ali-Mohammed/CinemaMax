using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaMaxFeeder;
using CinemaMaxFeeder.Database.Model;

namespace CinemaMax.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {


            var totalCompleteVideo = await _context.Movies.Where(Q => Q.DownloadStatus == MovieDownloadStatus.Complete).CountAsync();
            var totalWaitingVideo = await _context.Movies.Where(Q => Q.DownloadStatus == MovieDownloadStatus.Waiting).CountAsync();
            var moviesCompleteList = await _context.Movies.Where(Q => Q.DownloadStatus == MovieDownloadStatus.Complete).ToListAsync();

            return Ok( new { TotalDownloaded = totalCompleteVideo, TotalWaitingVideo= totalWaitingVideo, MoviesCompleteList=moviesCompleteList });


            return View(await _context.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nb,Priority,IsSlideShow,DownloadStatus,EnTitle,ArTitle,OtherTitle,Stars,EnTranslationFile,ArTranslationFile,FileFile,ArContent,EnContent,MDate,Year,Kind,Season,Img,ImgThumb,ImgMediumThumb,ImgObjUrl,ImgMediumThumbObjUrl,FilmRating,SeriesRating,EpisodeNummer,Rate,IsSpecial,ItemDate,Duration,ImdbUrlRef,ImgBanner,RootSeries,UseParentImg,SpTranslationFile,ShowComments,EpisodeFlag,Trailer,AudioStreamNumber,ParentSkipping,CollectionId,IsDeleted,CacheShort,ImgThumbObjUrl,ArTranslationFilePath,EnTranslationFilePath,HasIntroSkipping,VideoLikesNumber,VideoDisLikesNumber,VideoCommentsNumber,VideoViewsNumber,Castable,StartDownloadAt,FinishDownloadAt,DownloadRetry,DownloadId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nb,Priority,IsSlideShow,DownloadStatus,EnTitle,ArTitle,OtherTitle,Stars,EnTranslationFile,ArTranslationFile,FileFile,ArContent,EnContent,MDate,Year,Kind,Season,Img,ImgThumb,ImgMediumThumb,ImgObjUrl,ImgMediumThumbObjUrl,FilmRating,SeriesRating,EpisodeNummer,Rate,IsSpecial,ItemDate,Duration,ImdbUrlRef,ImgBanner,RootSeries,UseParentImg,SpTranslationFile,ShowComments,EpisodeFlag,Trailer,AudioStreamNumber,ParentSkipping,CollectionId,IsDeleted,CacheShort,ImgThumbObjUrl,ArTranslationFilePath,EnTranslationFilePath,HasIntroSkipping,VideoLikesNumber,VideoDisLikesNumber,VideoCommentsNumber,VideoViewsNumber,Castable,StartDownloadAt,FinishDownloadAt,DownloadRetry,DownloadId")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(long id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
