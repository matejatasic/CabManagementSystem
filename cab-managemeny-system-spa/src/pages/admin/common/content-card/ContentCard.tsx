import "./ContentCard.scss"
import ContentCardProps from "./ContentCardProps";

export default function ContentCard(props: ContentCardProps) {
    const { children } = props;

    return (
        <div id="content-card" className="mx-auto p-2">
            { children }
        </div>
    );
}