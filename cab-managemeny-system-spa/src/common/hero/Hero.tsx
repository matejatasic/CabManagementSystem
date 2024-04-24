import "./Hero.scss"
import HeroProps from "./HeroProps";

export default function Hero(props: HeroProps) {
    const { image, heading } = props;

    return (
        <div>
            <img id="header-image" src={image} alt="" />
            <div id="header-text" className="row px-3">
                <div className="col-12">
                    <h1 className="text-center">{ heading }</h1>
                </div>
            </div>
        </div>
    )
}