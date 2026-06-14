

function setChairs(capacity: number): React.ReactNode{

    const attributes = {
        width: 10,
        height: 5,
        fill: "brown",
    }

    const chairs = [];

    for(let i = 0; i < capacity; i++){
        if( i % 2 === 0){
            // top chair
            chairs.push(
                <rect
                    x = {i * 10}
                    y = {-5}
                    {...attributes}
                />
            )
        } else {
            // bottom chair
            chairs.push(
                <rect
                    x = {(i - 1) * 10}
                    y = {50}
                    {...attributes}
                />
            )
        }
    }

    return chairs;
}

export default function Table(props: Table){
    return(
        <g transform={`translate(${props.x}, ${props.y})`}>
            { setChairs(props.capacity) }
            <rect
                x = {0}
                y = {0}
                width={(props.capacity * 10) - 10}
                height={50}
                fill="lightgray"
                stroke="black"
            />
        </g>
    )
}