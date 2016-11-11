
import urllib2, bs4, re, csv

# bjjheroes -> inits parsing webcontent and writing csv
def initBjjheroesParser():
    conf = {
        'mainUrl': 'http://www.bjjheroes.com',
        'listUrlAppendix': '/a-z-bjj-fighters-list',
        'listContainer': '#tablepress-8',
        'listLinkSubstrings': {'required': '_empty_', 'forbidden': '/featured/'}}
    links = MainParser(conf).returnFighterLinks()
    fighters = BjjheroesFighterParser(links).returnFighters()
    csvWriter(fighters).writeCSV()

# stores name and fights of a single fighter
class Fighter():
    def __init__(self, name, fights):
        self.name = name
        self.fights = fights

# sends a request, waits for the response
# returns web content
class WebContentGrabber(object):
    def __init__(self, object):
        self.url = object
        self.headers = {'User-Agent': 'Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11',
           'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
           'Accept-Charset': 'ISO-8859-1,utf-8;q=0.7,*;q=0.3',
           'Accept-Encoding': 'none',
           'Accept-Language': 'en-US,en;q=0.8',
           'Connection': 'keep-alive'}

    def returnWebContent(self):
        self.request = urllib2.Request(self.url, headers = self.headers)
        try:
            self.response = urllib2.urlopen(self.request)
            return {'content': bs4.BeautifulSoup(self.response, 'html.parser'), 'failed': False}
        except:
            print 'Error: Could not open ' + self.url
            return {'failed': True}

# parses a website containing a list, table, etc with links to fighter profiles
# returns a list with links to fighter profiles
class MainParser(object):
    def __init__(self, object):
        self.maUrl = object['mainUrl']
        self.liUrl = object['listUrlAppendix']
        self.liCon = object['listContainer']
        self.liInf = object['listLinkSubstrings']

    def returnFighterLinks(self):
        webSource = WebContentGrabber(self.maUrl+self.liUrl).returnWebContent()
        if webSource['failed']:
            return
        fighterLinkTags = webSource['content'].select_one(self.liCon).find_all('a')    
        allLinks = list(set([linkTag['href'].encode('iso-8859-1') for linkTag in fighterLinkTags]))
        unconvertedFighterLinks = [link if self.liInf['required'] in link or self.liInf['forbidden'] not in link else '' for link in allLinks]
        fighterLinks = [link if self.maUrl in link else self.maUrl+link for link in unconvertedFighterLinks]
        return fighterLinks

# parses fighter profiles from bjjheroes
# returns a list of Fighter-instances including name and match infos
class BjjheroesFighterParser(list):
    def __init__(self, list):
        self.fiLis = list
        self.namId = {'class': '.heading', 'contains': ' Fight History'}
        self.fiLId = '.table.table-striped.sort_table'

    def returnFighters(self):
        fighters = [self.returnFighter(link) for link in self.fiLis]
        return fighters

    def returnFighter(self, link):
        webSource = WebContentGrabber(link).returnWebContent()
        if webSource['failed']:
            return
        fighterInfos = webSource['content'].select(self.namId['class']) and webSource['content'].find(string=re.compile(self.namId['contains']))
        try:
            infos = self.returnFighterDetails(webSource, fighterInfos)
        except:
            return
        return Fighter(infos['name'],infos['fights'])

    def returnFighterDetails(self, webSource, fighterInfos):
        name = fighterInfos.split(self.namId['contains'])[0]
        fightHistory = webSource['content'].select_one(self.fiLId).select('tr')[1:]
        fights = [self.returnFightDetails(fight, name) for fight in fightHistory]
        return {'fights': fights, 'name': name}

    def returnFightDetails(self, fight, name): 
        infos = fight.select('td')
        fightDetails = []
        fightDetails.append(name)
        fightDetails.append(infos[1].select('span')[0].contents[0])
        fightDetails.append(infos[2].contents[0])
        fightDetails.append(infos[3].contents[0])
        fightDetails.append(infos[4].contents[0])
        fightDetails.append(infos[5].contents[0])
        fightDetails.append(infos[6].contents[0])
        fightDetails.append(infos[7].contents[0])
        return fightDetails

# writes single fights rowwise into csv
class csvWriter(list):
    def __init__(self, list):
        self.fiLis = list
        self.csvFi = open("fights.csv", "wb")
        self.write = csv.writer(self.csvFi)

    def writeCSV(self):
        for fighter in self.fiLis:
            if fighter == None:
                continue
            fights = fighter.__dict__['fights']
            for fight in fights:
                row = ()
                for column in fight:
                	try:
                		row += (column.encode('iso-8859-1'),)
                	except:
                		row += ('',)
                self.write.writerow((row))
        self.csvFi.close()


if __name__=="__main__":
    initBjjheroesParser()
