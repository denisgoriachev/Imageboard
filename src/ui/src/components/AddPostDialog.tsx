import React, { useState } from 'react';
import { Dialog, AppBar, Toolbar, IconButton, Typography, Slide, Box } from '@material-ui/core';
import { ArrowBack } from '@material-ui/icons';
import { TransitionProps } from '@material-ui/core/transitions';
import { AddPostFormProps, AddPostForm } from './AddPostForm';

interface AddPostProps extends AddPostFormProps {
    open: boolean,
    title: string,
    changeVisibilityHandler: (value: boolean) => void
}

const Transition = React.forwardRef(function Transition(
    props: TransitionProps & { children?: React.ReactElement },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

export const AddPostDialog: React.FC<AddPostProps> = (props) => {
    const [isOpen, setIsOpen] = useState(props.open)

    return (
        <Dialog fullScreen open={isOpen} TransitionComponent={Transition}>
            <AppBar style={{ position: "relative" }}>
                <Toolbar>
                    <IconButton edge="start" color="inherit" onClick={() => { setIsOpen(false); props.changeVisibilityHandler(false); }} aria-label="close">
                        <ArrowBack />
                    </IconButton>
                    <Typography variant="h6" style={{ flex: 1 }}>
                        {props.title}
                    </Typography>
                </Toolbar>
            </AppBar>
            <Box m={1}>
                <AddPostForm topicId={props.topicId} parentPost={props.parentPost} onSubmitted={props.onSubmitted} />
            </Box>
        </Dialog>
    )
}