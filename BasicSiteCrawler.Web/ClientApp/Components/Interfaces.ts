export interface IAppState {
    urls: Array<IUrl>
}

export interface INavigationProps {
    onBtnClick: () => void
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