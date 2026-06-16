

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

export default function Stations(props: Station){

    const handleClick = () => {
        console.log(`Station ${props.id} clicked!`);
    }

    return(
        <g transform={`translate(${props.x}, ${props.y})`}>
            { setChairs(props.capacity) }
            <rect className="cursor-pointer fill-gray-400 hover:fill-gray-500"
                x = {0}
                y = {0}
                width={(props.capacity * 10) - 10}
                height={50}
                onClick={handleClick}
            />
        </g>
    )
}
    
