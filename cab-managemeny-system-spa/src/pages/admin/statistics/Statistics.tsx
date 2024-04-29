import "./Statistics.scss"

export default function Statistics() {
    const statisticsItems = [
        {
            label: "Users",
            count: 0
        },
        {
            label: "Employees",
            count: 0
        },
        {
            label: "Cars",
            count: 0
        },
        {
            label: "Routes taken",
            count: 0
        },
    ];

    return (
        <div className="row">
            {statisticsItems.map(item => {
                return (
                    <div className="col-12 col-md-6 mb-3" key={item.label}>
                        <div className="card w-100">
                            <div className="card-body text-center">
                                <h2 className="card-title">{item.label}</h2>
                                <p className="card-text statistic-count">{item.count}</p>
                            </div>
                        </div>
                    </div>
                );
            })}
        </div>
    );
}