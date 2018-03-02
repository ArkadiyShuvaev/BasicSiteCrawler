import * as React from "react";
import Navigation from "./Navigation";
import {UrlList} from "./UrlList";
import { IUrl, IAppState } from "./Interfaces";



export default class App extends React.Component<{}, IAppState> {

    constructor(props: {}) {
        super(props);

        const urls: Array<IUrl> = [{ url: "http://company.com/link1", id: "1" } as IUrl,
            { url: "http://company.com/link2", id: "2" } as IUrl];

        const initialState = {
            urls: urls
        }
        this.state = initialState;
    }


    render() {
        
        return <div className="container-fluid">
            <div className="row">
                <div className="col-xs-12">
                    <Navigation onBtnClick= {() => { alert("btn click!"); }}/>
                </div>
            </div>
            <div className="row">
                <div className="col-xs-12">
                    <UrlList rows={this.state.urls} />
                </div>
            </div>
        </div>;
    }
}
