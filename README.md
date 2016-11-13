# BjjElo
A ranking system for Brazilian Jiu Jitsu-Competitors.

# Introduction
The Elo rating system (https://en.wikipedia.org/wiki/Elo_rating_system) is known from chess and provides a measure of a player's strength. Whenever two competitors play a ranked match, one of them gains points, while the other one loses the same amount. The more matches are being ranked, the more precise the rankings become. Ratings are about 800 for beginners, 1600 for mid-level players and 2400 for professionals.

# Elo outside of chess
The elo system can be adapted for other kind of competitions besides chess (see https://en.wikipedia.org/wiki/Elo_rating_system#Use_outside_of_chess). In combat sports, the site http://www.mma-elo.com/ aims to provide Elo rankings of MMA competitors.

# Elo for Brazilian Jiu Jitsu
In one way, BJJ is similiar to chess: A match will always end with one of the competitors being victorious, or sometimes (if allowed) with a draw. As such, there isn't a problem with implementing the Elo rating system for BJJ. In contrast to MMA, where a competitor usually only has like 10-30 matches in his whole career, a BJJ competitor usually has multiple fights in a single tournament, and can easily participate in a few tournaments per year. The number of matches being ranked would therefore be high enough to calculate a relatively precise elo Rating.

# Benefits of the proposed rating
The Elo rating could provide a good measure of a competitors strength. Of course, the different belt colours (White, Blue, Purple, Brown, Black) should do exactly that, however, those are by nature subjective by the teachers individual rating of his students. Furthermore, a Elo rating would allow to include competitors from similar combat sports like Luta Livre or Judo and to rank them together with the BJJ practitioners. By having a more precise rating of a competitor, tournament classes (beginner, advanced, elite, ...) could potentially be divided by Elo instead of belts.

# Project status
Currently, I am just testing the concept. The most difficult part is to gather example data, since complete brackets are only seldomly published. I found some single match results for the World Championships 2016 at http://www.bjjheroes.com/bjj-news/ibjjf-2016-world-jiu-jitsu-championship-full-results-buchecha-makes-history, and at the same website, there are listings of matches for individual fighters, e.g. http://www.bjjheroes.com/bjj-fighters/roger-gracie-bio. If you found other sources, please let me know!

# More datasources
* IBJJF Tournament Brackets: http://events.ibjjf.com/, e.g. http://events.ibjjf.com/sportos/application/sheet/index.php?idevent=434&dayofevent=2&temp=true
