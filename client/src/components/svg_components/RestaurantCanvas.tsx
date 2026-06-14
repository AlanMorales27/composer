/**
 * This component represents the canvas where the restaurant layout is displayed
 */

import { useState } from "react";
import Table from "./Table";

export default function RestaurantCanvas(){

    const [tables, setTables] = useState<Table[]>([
        {
            id: 1,
            x: 100,
            y: 100,
            capacity: 4
        }
    ]);

    return (
        <svg className="bg-gray-100"
            width="800"
            height="600"
        >
            {tables.map(table => ( <Table {...table}/> ))}
    
            
        </svg>
    )
}