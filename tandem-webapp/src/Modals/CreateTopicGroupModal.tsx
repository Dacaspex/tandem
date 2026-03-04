import {useState} from "react";

interface CreateTopicGroupModalProps {
  onSubmit: (topicName: string) => Promise<void>;
  onCancel: () => void;
}

export default function CreateTopicGroupModal({ onSubmit, onCancel }: CreateTopicGroupModalProps) {
  const [topicGroupName, setTopicGroupName] = useState('');

  return (
    <div>
      <div className='mb-0'>Topic group name</div>
      <input
        className='form-control form-control-inline mb-1'
        name='topic-name'
        type='text'
        value={ topicGroupName }
        onChange={ (e) => setTopicGroupName(e.target.value) }
      />
      <div className='button-row button-row-reverse'>
        <div
          className='button button-primary button-s'
          onClick={ () => onSubmit(topicGroupName)}>
          Submit
        </div>
        <div
          className='button button-s'
          onClick={ onCancel }>
          Cancel
        </div>
      </div>
    </div>
  )
}