import * as React from "react";
import { IUrlListProps } from "./Interfaces";
import UrlListRow from "./UrlListRow";

export class UrlList extends React.PureComponent<IUrlListProps> {

    constructor(props: IUrlListProps) {
        super(props);
    }

    render() {
        const rows = this.props.rows.map((item) => {
            return (<UrlListRow key={item.id} url={item.url} />);
        });
        return (
            <div>
                <React.Fragment>
                    {rows}
                </React.Fragment>
            </div>
        );
    }
}



//// ReSharper disable once InconsistentNaming
//export const UrlList: React.SFC<IUrlListProps> = (props) => {
//    const rows = props.rows.map((item) => {
//        return (<UrlListRow id={item.id} url={item.url} />);
//    });
//    return (
//        <div>
//        <React.Fragment>
//            {rows}
//        </React.Fragment>
//        </div>
//    );
//}
