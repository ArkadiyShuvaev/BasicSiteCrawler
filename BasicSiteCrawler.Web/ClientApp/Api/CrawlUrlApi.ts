import RequestService from "./RequestService";

const baseUrl = "/StartCrawl";

export default class CrawlUrlApi {

    async startCrawlUrlAsync(startingUrl: string): Promise<boolean> {

        return new Promise((resolve: (value?: boolean | PromiseLike<boolean>) => void, reject: (value?: Error) => void) => {
            const dto = { startingUrl: startingUrl };

            const requestService = new RequestService();
            const result = requestService.postAsync(baseUrl, JSON.stringify(dto));
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
