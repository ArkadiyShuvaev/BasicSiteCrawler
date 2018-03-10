import * as React from "react";
import Navigation from "./StaringUrlForm";
import {UrlList} from "./UrlList";
import { IUrl, IAppState, IFormData } from "./Interfaces";
import CrawlUrlApi from "../Api/CrawlUrlApi";

const signalR = require("@aspnet/signalR");
//let signalR: any;


export default class App extends React.Component<{}, IAppState> {
    
    constructor(props: {}) {
        super(props);
        
        const urls: Array<IUrl> = [{ url: "http://company.com/link1", id: "1" } as IUrl,
            { url: "http://company.com/link2", id: "2" } as IUrl];

        const initialState = {
            urls: urls,
            formData: {
                inputFormValue: "",
                isInputDisabled: false
            } as IFormData
        }

        this.state = initialState;
        this.handleFormClickNavigation = this.handleFormClickNavigation.bind(this);
        this.handleInputFormChange = this.handleInputFormChange.bind(this);

        const urlConnection = new signalR.HubConnection("/CrawlUrlHub");
        urlConnection.on("send", (data: string): void => {
            this.addItemToStart({ url: data, id: data });
        });
        urlConnection
            .start()
            .then(() => urlConnection.invoke("send", "http://test.com/url1"));
    }

    addItemToStart(url: IUrl) {
        const newUrlList = [url, ...this.state.urls];
        this.setState({
            urls: newUrlList
        });
    }

    handleInputFormChange(e: React.FormEvent<HTMLInputElement>) {
        const newFormData = Object.assign(this.state.formData, {
            inputFormValue: e.currentTarget.value
        } as IFormData);

        this.setState({
            formData: newFormData
        });
    }

    async handleFormClickNavigation(e: React.FormEvent<HTMLInputElement>) {
        e.preventDefault();
        const crawlUrlApi = new CrawlUrlApi();

        const isSuccessfull = await crawlUrlApi.startCrawlUrlAsync(this.state.formData.inputFormValue);
            
        const newFormData = Object.assign(this.state.formData, {
            isInputDisabled: isSuccessfull
        } as IFormData);

        this.setState({
            formData: newFormData
        });

        //if (isSuccessfull) {
        //    let i = 3;
        //    setInterval(() => {
        //        this.addItemToStart({ url: "http://company.com/link" + i, id: (i++).toString() } as IUrl);
        //    }, 2000);
        //}
            
    }


    render() {
        
        return <div className="container">
            <div className="row">
                <div className="col-xs-12">
                    <div className="panel panel-primary">
                        <div className="panel-body">
                            <Navigation
                                inputFormValue={this.state.formData.inputFormValue}
                                onChange={this.handleInputFormChange}
                                onBtnClick={this.handleFormClickNavigation}
                                isInputDisabled={this.state.formData.isInputDisabled} />
                        </div>
                    </div>
                </div>
            </div>
            <div className="row">
                <div className="col-xs-12">
                    <div className="panel panel-info">
                        <div className="panel-body">
                            <UrlList rows={this.state.urls} />
                        </div>
                    </div>
                </div>
            </div>
        </div>;
    }
}
