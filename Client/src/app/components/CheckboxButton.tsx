import { FormGroup, FormControlLabel, Checkbox } from "@mui/material";
import { useState } from "react";

interface Props{
    items: string[],
    checked?: string[],
    onChange: (items: string[]) => void;
}

export default function checkboxButtons({items, checked, onChange}: Props){
    const [checkedItems, setChekedItems] = useState(checked || [])

    function handelChecked(value: string){
        const currentndex = checkedItems.findIndex(item => item === value);
        let newChecked: string[] = [];
        if(currentndex === - 1) newChecked = [...checkedItems, value];
        else newChecked = checkedItems.filter(items => items !== value);
        setChekedItems(newChecked);
        onChange(newChecked);
    }

    return(
        <FormGroup>
            {items.map(item => (
                <FormControlLabel 
                    control={<Checkbox 
                        checked={checkedItems.indexOf(item) !== -1}
                        onClick={() => handelChecked(item)}
                    />} 
                    label={item} 
                    key={item} 
                />
            ))}
        </FormGroup>
        )
}
    