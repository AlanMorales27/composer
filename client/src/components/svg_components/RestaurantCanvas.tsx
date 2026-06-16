/**
 * This component represents the canvas where the restaurant layout is displayed
 */

import { useState } from "react";
import Stations from "./Station";

export default function RestaurantCanvas(){

    const [stations, setStations] = useState<Station[]>([
        {
            id: 1,
            x: 100,
            y: 100,
            capacity: 4
        },
        {
            id: 2,
            x: 100,
            y: 200,
            capacity: 8
        },
        {
            id: 2,
            x: 100,
            y: 300,
            capacity: 14
        }
    ]);

    return (
        <svg className="bg-[#2D323E]"
            width="800"
            height="600"
        >
            {stations.map(station => ( <Stations {...station}/> ))}
        </svg>
    )
}