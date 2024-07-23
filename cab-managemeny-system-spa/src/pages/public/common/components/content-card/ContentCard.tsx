import "./ContentCard.scss"
import ContentCardProps from "./ContentCardProps";

export default function ContentCard(props: ContentCardProps) {
    const { heading, children } = props;

    return (
        <div id="content-card" className="mx-auto p-2">
            <h2 id="content-card-heading" data-testid="heading" className="text-center">{ heading }</h2>
            <hr />
            { children }
        </div>
    );
}