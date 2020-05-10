import React from 'react';
import { PostDto } from '../api/ImageboardClient';
import { Box, useTheme } from '@material-ui/core';
import { PostCard } from './PostCard';

interface PostProps {
    data: PostDto,
    deep: number,
    onSubmitted: (postId: number) => void
}

export const Post: React.FunctionComponent<PostProps> = (props) => {
    const theme = useTheme();
    
    return (
        <Box>
            <Box mb={2} ml={theme.spacing(0.25) * props.deep}>
                <PostCard data={props.data} showActions={props.deep < 6} onSubmitted={props.onSubmitted} />
            </Box>
            {props.data.children?.map((childPost) => <Post onSubmitted={props.onSubmitted} key={childPost.id} deep={props.deep + 1} data={childPost} />)}
        </Box>
    )
}