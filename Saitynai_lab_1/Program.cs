using Saitynai_lab_1.Data;
using Saitynai_lab_1.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<BooksDbContext>();

builder.Services.AddTransient<IBooksRepository, BooksRepository>();
builder.Services.AddTransient<IReviewsRepository, ReviewsRepository>();
builder.Services.AddTransient<IReviewScoresRepository, ReviewScoresRepository>();

var app = builder.Build();

app.MapControllers();

app.UseRouting();

app.Run();