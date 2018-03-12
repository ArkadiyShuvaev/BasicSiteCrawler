import {UrlCollection} from "../UrlCollection";

export interface IFormData {
    inputFormValue: string;
    isUrlProcessing: boolean;
}

export interface IAppState {
    urls: UrlCollection;
    formData: IFormData;
}

export interface IStaringUrlFormProps {
    inputFormValue: string;
    isUrlProcessing: boolean;
    onChange: (e: React.FormEvent<HTMLInputElement>) => void;
    onStartBtnClick: (e: React.FormEvent<HTMLInputElement>) => void;
    onStopBtnClick: (e: React.FormEvent<HTMLInputElement>) => void;
}

export interface IUrlListRowProps {
    url: string;
}

export interface IUrlListProps {
    rows: Array<IUrl>
}

export interface IUrl {
    url: string;
    id: string;
}