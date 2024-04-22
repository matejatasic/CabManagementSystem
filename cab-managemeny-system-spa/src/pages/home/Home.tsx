import "./Home.scss";
import Hero from "../../common/hero/Hero";
import image from "../../assets/images/home-hero.jpg"
import Footer from "../../common/Footer";

export default function Home() {
    return (
        <div>
            <Hero image={image} heading="Cab Management System" />
            <main className="container">
                <div id="content-card" className="mx-auto my-5 p-2">
                    <h2 id="content-card-heading" className="text-center">Welcome to the Cab management system</h2>
                    <hr />
                    <div className="text-start">
                        <p>
                            Lorem, ipsum dolor sit amet consectetur adipisicing elit. Eos numquam minima
                            quisquam suscipit unde, est molestias non dignissimos soluta deleniti hic
                            ratione maiores cum in praesentium voluptatibus dolore dolor odit iusto nisi
                            incidunt dolorum corporis velit. Aliquid officia aliquam nemo excepturi,
                            reiciendis possimus tenetur animi quis laudantium, architecto quos quo?
                        </p>
                        <p>
                            Proin quis porttitor risus. Etiam convallis lorem pharetra sodales sagittis.
                            Nullam sed molestie augue, eu elementum lectus. Vestibulum ante ipsum primis
                            in faucibus orci luctus et ultrices posuere cubilia curae; Lorem ipsum dolor sit amet,
                            consectetur adipiscing elit. Fusce non metus id elit sagittis porta.
                            Phasellus eu leo ultrices, rutrum erat eget, fermentum massa. Nulla facilisi.
                            Donec vel sem tellus. Quisque euismod lacus finibus ante fermentum aliquet sit amet ac enim.
                            Morbi et quam hendrerit, mattis libero eget, ornare ex.
                        </p>
                        <p>
                            Nulla nec porta purus. Suspendisse potenti. Donec elementum fermentum metus in convallis.
                            Aliquam erat volutpat. Proin eget pharetra ligula. Vivamus tincidunt mollis neque id dictum.
                            Phasellus eu laoreet lacus. Aenean in mattis dolor. In eu lobortis ipsum, nec ultricies sapien.
                            Duis ut volutpat sapien, id faucibus tortor. Cras ipsum urna, pellentesque ac finibus non, viverra ac magna.
                            Nulla malesuada porta pharetra. Fusce euismod ipsum ac fermentum bibendum.
                        </p>
                        <p>
                            Praesent urna diam, gravida eu ipsum eu, tristique sodales massa.
                            Nulla vitae massa viverra erat tempus sollicitudin sit amet vitae tortor.
                            Integer euismod sapien vitae dui pharetra dignissim. Praesent mauris lectus,
                            ullamcorper sed rutrum quis, maximus eget orci. Aenean sagittis ac neque id feugiat.
                            Maecenas et dictum ante, quis ornare est. Nunc ligula nulla, aliquet eu venenatis sed,
                            auctor non ante.
                        </p>
                    </div>
                </div>
            </main>
            <Footer />
        </div>
    );
}