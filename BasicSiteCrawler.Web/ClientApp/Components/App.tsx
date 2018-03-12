import * as React from "react";
import StaringUrlForm from "./StaringUrlForm";
import {UrlList} from "./UrlList";
import { IAppState, IFormData } from "./Interfaces";
import CrawlUrlApi from "../Api/CrawlUrlApi";
import {UrlCollection} from "../UrlCollection";

const signalR = require("@aspnet/signalR");
//let signalR: any;


export default class App extends React.Component<{}, IAppState> {
    
    constructor(props: {}) {
        super(props);
        
        const initialState = {
            urls: new UrlCollection(),
            formData: {
                inputFormValue: "",
                isUrlProcessing: false
            } as IFormData
        }

        this.state = initialState;
        this.handleStartBtnClickForm = this.handleStartBtnClickForm.bind(this);
        this.handleInputFormChange = this.handleInputFormChange.bind(this);
        this.handleStopBtnClickForm = this.handleStopBtnClickForm.bind(this);

        const urlConnection = new signalR.HubConnection("/CrawlUrlHub");
        urlConnection.on("send", (data: string): void => {
            this.state.urls.addItemToStart(data);
            this.setState({
                urls: this.state.urls
            });
            
        });
        urlConnection
            .start()
            .then(() => urlConnection.invoke("send", "http://test.com/url1"));
    }

    handleInputFormChange(e: React.FormEvent<HTMLInputElement>) {
        const newFormData = Object.assign(this.state.formData, {
            inputFormValue: e.currentTarget.value
        } as IFormData);

        this.setState({
            formData: newFormData
        });
    }

    async handleStopBtnClickForm(e: React.FormEvent<HTMLInputElement>) {
        e.preventDefault();
        const crawlUrlApi = new CrawlUrlApi();
        const isSuccessfull = await crawlUrlApi.stopCrawlUrlAsync();

        const newFormData = Object.assign(this.state.formData, {
            isUrlProcessing: !isSuccessfull
        } as IFormData);

        this.setState({
            formData: newFormData
        });

    }

    async handleStartBtnClickForm(e: React.FormEvent<HTMLInputElement>) {
        e.preventDefault();
        const crawlUrlApi = new CrawlUrlApi();

        const isSuccessfull = await crawlUrlApi.startCrawlUrlAsync(this.state.formData.inputFormValue);
            
        const newFormData = Object.assign(this.state.formData, {
            isUrlProcessing: isSuccessfull
        } as IFormData);

        this.setState({
            formData: newFormData
        });
        
    }


    render() {
        
        return <div className="container">
            <div className="row">
                <div className="col-xs-12">
                    <div className="panel panel-primary">
                        <div className="panel-body">
                            <StaringUrlForm
                                inputFormValue={this.state.formData.inputFormValue}
                                onChange={this.handleInputFormChange}
                                onStartBtnClick={this.handleStartBtnClickForm}
                                isUrlProcessing={this.state.formData.isUrlProcessing}
                                onStopBtnClick={this.handleStopBtnClickForm}/>
                        </div>
                    </div>
                </div>
            </div>
            <div className="row">
                <div className="col-xs-12">
                    <div className="panel panel-info">
                        <div className="panel-body">
                            <UrlList rows={this.state.urls.items} />
                        </div>
                    </div>
                </div>
            </div>
        </div>;
    }
}
