export default class RequestService {
    
    async postAsync(url: string, postBody: string):Promise<any> {

        const myInit: RequestInit = {
            body: postBody,
            cache: "no-cache",
            headers: {
                "content-type": "application/json"
            },
            method: "POST",
            referrer: "no-referrer"
        };

        const response = await fetch(url, myInit);
        return await response.json();
    }
}
