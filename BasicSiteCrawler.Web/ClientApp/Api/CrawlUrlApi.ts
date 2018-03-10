import RequestService from "./RequestService";

const baseUrl = "/api/crawl";

export default class CrawlUrlApi {

    async startCrawlUrlAsync(startingUrl: string): Promise<boolean> {

        return new Promise((resolve: (value?: boolean | PromiseLike<boolean>) => void, reject: (value?: Error) => void) => {
            const dto = { startingUrl: startingUrl };

            const requestService = new RequestService();
            const url = `${baseUrl}/startcrawl`;
            const result = requestService.postAsync(url, JSON.stringify(dto));
            result.then(result => {
                //todo logging 
                resolve(true);
            }).catch(error => {
                //todo logging 
                resolve(false);
            });
        });
    }
}
